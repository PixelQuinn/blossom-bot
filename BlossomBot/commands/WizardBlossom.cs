using System;
using System.Collections.Generic;
using System.Reflection.Emit;
using System.Security.Policy;
using System.Threading.Tasks;
using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using Google.Apis.Discovery;
using static System.Net.Mime.MediaTypeNames;

public class WizardBlossomCommands : BaseCommandModule
{
    private static readonly string[] WizardBlossomResponses = {
        "Indeed, the crystal ball reveals a definite yes!",
        "Alas, the mystical energies suggest a negative response.",
        "I sense a foggy future, ask again later.",
        "The magical energies are not clear at the moment, try again.",
        "The stars align in favor, most likely!",
        "The cosmic forces deny this outcome, don't count on it.",
        "I see a magical path leading to a positive answer.",
        "The cosmic winds are uncertain, reply hazy, try again."
    };

    [Command("wizardblossom")]
    [Description("Consult Wizard Blossom to predict the future.")]
    public async Task WizardBlossomCommand(CommandContext ctx, [RemainingText] string question)
    {
        Random random = new Random();
        int index = random.Next(WizardBlossomResponses.Length);
        string response = WizardBlossomResponses[index];

        await ctx.RespondAsync($"🔮 **Question:** {question}\n**Answer:** {response}");
    }

    [Command("brewpotion")]
    [Description("Brew a potion with the cat wizard's alchemical skills.")]
    public async Task BrewPotionCommand(CommandContext ctx)
    {
        // Simulate a variety of potion ingredients
        string[] potionIngredients = { "Feline Whisker", "Stardust Catnip", "Moonbeam Essence", "Enchanted Milk", "Shadowy Purrfectness" };

        // Randomly select ingredients for the potion
        Random random = new Random();
        string ingredient1 = potionIngredients[random.Next(potionIngredients.Length)];
        string ingredient2 = potionIngredients[random.Next(potionIngredients.Length)];

        // Generate a whimsical potion name
        string potionName = $"{ingredient1} and {ingredient2} Elixir";

        // Your potion-brewing logic here

        // Simulate a brewing process (you can replace this with your own logic)
        await Task.Delay(2000); // Simulating a brewing time

        // Announce the completion of the potion
        await ctx.RespondAsync($"🧪✨ The cat wizard brews a {potionName}! It sparkles with magical energy. Drink at your own whiskerilicious risk!");
    }

    [Command("summonfamiliar")]
    [Description("Summon a magical cat familiar to aid you.")]
    public async Task SummonFamiliarCommand(CommandContext ctx)
    {
        // Simulate the summoning process (you can replace this with your own logic)
        await ctx.RespondAsync("🔮✨ Conjuring magical energies...");

        // Simulate a delay to create suspense during the summoning
        await Task.Delay(3000);

        // Announce the appearance of the familiar
        await ctx.RespondAsync("🐱✨ A magical cat familiar appears by your side. Meowgical!");
    }

    public class Spell
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int Level { get; set; }
    }

        List<Spell> spells = new List<Spell>
    {
        // Level 1 Spells
        new Spell { Name = "Purrtection", Description = "Surround yourself with a soothing purr, granting advantage on Charisma saving throws and making you irresistible to feline creatures.", Level = 1 },
        new Spell { Name = "Catnip Bolt",  Description = "Hurl a magical bolt infused with the essence of catnip at a target, dealing 1d8 psychic damage. On a failed Wisdom saving throw, the target is distracted and has disadvantage on its next attack roll.", Level = 1 },
        new Spell { Name = "Mage Paw",  Description = "Transform the touched object into a small, harmless cat statue. While in this form, the object retains its properties but gains an adorable feline appearance.", Level = 1},
        new Spell { Name = "Landing on All Fours", Description = "When a creature falls, gently guide them to the ground as if they always land on their feet. The fall becomes slow and controlled, preventing fall damage.", Level = 1},
        new Spell { Name = "Furry Friends",  Description = "Summon a swarm of playful spectral cats to distract and harass enemies in the area. Creatures within the swarm's space must succeed on a Dexterity saving throw or take 1d6 slashing damage.", Level = 1},

        // Level 2 Spells
        new Spell { Name = "Telepawtation", Description = "Teleports the caster using kitty magic to a location they can see.",  Level = 2 },
        new Spell { Name = "Hissilient Image", Description = "Creates an illusory image of a hissing cat to distract and confuse enemies.", Level = 2 },
        new Spell { Name = "Clawmorph", Description = "Transform into a cat for a short duration, gaining increased agility and stealth. You can move through spaces as narrow as 1 inch wide without squeezing, and you have advantage on Dexterity (Stealth) checks.", Level = 2 },
        new Spell { Name = "Paws of Enfeeblement", Description = "Channel the weakness of a cat's touch to sap the strength of a creature within range. The target must succeed on a Constitution saving throw or have its Strength score reduced by 1d6 for the spell's duration.", Level = 2 },
        new Spell { Name = "Dopplepurr", Description = "Create an illusory duplicate of yourself that appears indistinguishable from you. The duplicate can move and mimic your actions, providing advantage on Dexterity saving throws until it is dispelled or takes damage.", Level = 2 },

        // Level 3 Spells
        new Spell { Name = "Whiskerwind Shield", Description = "Summons a protective shield made of magical whiskers that deflects attacks.", Level = 3 },
        new Spell { Name = "Catnap", Description = "Allows the caster and their allies to take a short rest in the blink of an eye.", Level = 3 },
        new Spell { Name = "Blinking Catwalk", Description = "Instantly teleport to an unoccupied space you can see within range, leaving behind an illusory image of a cat that confuses and distracts enemies. This teleportation does not provoke opportunity attacks.", Level = 3 },
        new Spell { Name = "Necro-purr-mancy", Description = "Raise a fallen feline ally as a spectral guardian. The cat companion has its own initiative and can make attacks, providing assistance to its allies. If it is reduced to 0 hit points, it vanishes, returning to the afterlife.", Level = 3 },
        new Spell { Name = "Purralysis Ray", Description = "Unleash a beam of purr energy at a target within range. On a hit, the target must succeed on a Constitution saving throw or be paralyzed for 1 minute. The target can repeat the saving throw at the end of each of its turns, ending the effect on a success.", Level = 3 },

        // Level 4 Spells
        new Spell { Name = "Dimensional Catnapping", Description = "Open a temporary portal to a cozy cat dimension. Allies within the area can use their reaction to step into the portal and take a short rest, gaining the benefits without spending any time.", Level = 4 },
        new Spell { Name = "Cat - astrophic Wave", Description = "Unleash a wave of magical energy in a cone, causing all creatures in its area to make a Dexterity saving throw.On a failed save, they take 6d6 force damage and are knocked prone.On a successful save, they take half damage and are not knocked prone.", Level = 4 },
        new Spell { Name = "Purrifying Flames", Description = "Ignite a target with magical flames that purify and heal. The target regains hit points equal to 4d8 + your spellcasting ability modifier, and any curses or diseases affecting the target are lifted.", Level = 4 },
        new Spell { Name = "Phantom Litter Box", Description = "Create an illusory litter box in an area. Enemies who enter the area must make a Wisdom saving throw or become frightened for 1 minute. A creature can repeat the saving throw at the end of each of its turns, ending the effect on a success.", Level = 4 },
        new Spell { Name = "Spectral Catwalk", Description = "Conjure a shimmering catwalk in the air. Allies can use their movement to walk along the catwalk without touching the ground, avoiding difficult terrain and gaining advantage on Dexterity (Stealth) checks while on the catwalk.", Level = 4 },

        // Level 5 Spells
        new Spell { Name = "Feline Familiar Swarm", Description = "Summon a swarm of spectral familiars in the form of various feline creatures. These familiars act as independent entities, each making attacks and providing assistance to the caster or their allies. The swarm disperses after 1 minute.", Level = 5 },
        new Spell { Name = "Cat's Grace", Description = "Channel the agility and grace of a cat, granting yourself or a willing creature you touch advantage on Dexterity saving throws and the ability to move across difficult terrain without taking damage or penalties for the duration.", Level = 5 },
        new Spell { Name = "Purrpetual Motion", Description = "Transform into an ethereal cat, granting yourself the ability to move through other creatures and objects as if they were difficult terrain. You can move through solid objects, and your attacks pass through armor and shields.", Level = 5 },
        new Spell { Name = "Whiskerstorm", Description = "Summon a magical storm of whiskers that swirls around a target area. Creatures within the storm must succeed on a Strength saving throw or be restrained for the spell's duration. A restrained creature can make a Strength saving throw at the end of each of its turns, ending the effect on a success.", Level = 5 },
        new Spell { Name = "Ninth Life", Description =  "Grant a target the blessing of multiple lives. If the target is reduced to 0 hit points within the spell's duration, they are instantly resurrected with half of their maximum hit points. This effect can only occur once during the spell's duration.", Level = 5 },

        // Level 6 Spells
        new Spell { Name = , Description = , Level = 6 },
        new Spell { Name = , Description = , Level = 6 },
        new Spell { Name = , Description = , Level = 6 },
        new Spell { Name = , Description = , Level = 6 },
        new Spell { Name = , Description = , Level = 6 },

        // Level 7 Spells
        new Spell { Name = , Description = , Level = 7 },
        new Spell { Name = , Description = , Level = 7 },
        new Spell { Name = , Description = , Level = 7 },
        new Spell { Name = , Description = , Level = 7 },
        new Spell { Name = , Description = , Level = 7 },

        // Level 8 Spells
        new Spell { Name = , Description = , Level = 8 },
        new Spell { Name = , Description = , Level = 8 },
        new Spell { Name = , Description = , Level = 8 },
        new Spell { Name = , Description = , Level = 8 },
        new Spell { Name = , Description = , Level = 8 },

        // Level 9 Spells
        new Spell { Name = , Description = , Level = 9 },
        new Spell { Name = , Description = , Level = 9 },
        new Spell { Name = , Description = , Level = 9 },
        new Spell { Name = , Description = , Level = 9 },
        new Spell { Name = , Description = , Level = 9 },

        // Big bad boss Spells
        new Spell { Name = , Description = , Level = 99 },
        new Spell { Name = , Description = , Level = 99 },
        new Spell { Name = , Description = , Level = 99 },
        new Spell { Name = , Description = , Level = 99 },
        new Spell { Name = , Description = , Level = 99 }
    };

    string[] catchphrases = { "Mrowl! I am the master of magic!", "Hiss! Tremble before my arcane might!" };
    string[] castingPhrases = { "Abracadabra!", "Meowgicus!", "Purrformus!" };

    [Command("spellbook")]
    [Description("Consult the cat wizard's spellbook for arcane knowledge.")]
    public async Task SpellbookCommand(CommandContext ctx, [RemainingText] string target = null)
    {
        // Delete the command message
        await ctx.Message.DeleteAsync();

        Random random = new Random();

        // Pick a random spell
        Spell randomSpell = spells[random.Next(spells.Count)];

        // Pick a random catchphrase
        string randomCatchphrase = catchphrases[random.Next(catchphrases.Length)];

        // Pick a random casting phrase
        string randomCastingPhrase = castingPhrases[random.Next(castingPhrases.Length)];

        // Construct the message with catchphrase
        string message = $"*Evil Cat Wizard Catchphrase:* {randomCatchphrase}";

        // Send the catchphrase
        var catchphraseMessage = await ctx.RespondAsync(message);

        // Pause for 2 seconds
        await Task.Delay(2000);

        // Construct the message with casting phrase
        message = $"*Casting Phrase:* {randomCastingPhrase}";

        // Edit the catchphrase message to include casting phrase
        await catchphraseMessage.ModifyAsync(message);

        // Pause for 2 seconds
        await Task.Delay(2000);

        // Construct the final spell casting message
        message = $"**Blossom casts {randomSpell.Name}**";

        // Include target information if specified
        if (!string.IsNullOrEmpty(target))
        {
            message += $" @ {target}";
        }

        // Send the final spell casting message
        await ctx.RespondAsync(message);
    }


}
